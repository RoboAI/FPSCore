using Ilumisoft.RadarSystem.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ilumisoft.RadarSystem
{
    [AddComponentMenu("Radar System/Radar")]
    [DefaultExecutionOrder(-10)]
    public class Radar : MonoBehaviour
    {
        /// <summary>
        /// Dictionary allowing to access the icon of a locatable
        /// </summary>
        readonly Dictionary<LocatableComponent, LocatableIconComponent> locatableIconDictionary = new();

        [SerializeField]
        [Tooltip("The container icons will be added to")]
        private RectTransform iconContainer;

        [SerializeField]
        [Tooltip("Compass")]
        private Image imageCompass;

        [SerializeField, Min(1)]
        [Tooltip("The detection range of the radar in meter")]
        private float range = 20;

        [SerializeField]
        [Tooltip("Whether the radar should apply the roation of the player")]
        private bool applyRotation = true;

        /// <summary>
        /// The detection range of the radar
        /// </summary>
        public float Range { get => range; set => range = value; }

        /// <summary>
        /// Whether the radar rotates with the player or not
        /// </summary>
        public bool ApplyRotation { get=> applyRotation; set => applyRotation = value; }

        /// <summary>
        /// Reference to the player
        /// </summary>
        public GameObject Player;

        public Dictionary<LocatableIconComponent, RectTransform> _rectTransforms;

        private void Start()
        {
        }

        private void OnEnable()
        {
            if (_rectTransforms == null)
                _rectTransforms = new Dictionary<LocatableIconComponent, RectTransform>();

            LocatableManager.OnLocatableAdded += OnLocatableAdded;
            LocatableManager.OnLocatableRemoved += OnLocatableRemoved;
        }

        private void OnDisable()
        {
            LocatableManager.OnLocatableAdded -= OnLocatableAdded;
            LocatableManager.OnLocatableRemoved -= OnLocatableRemoved;
        }

        /// <summary>
        /// Callback invoked when a locatable has been added
        /// </summary>
        /// <param name="locatable"></param>
        private void OnLocatableAdded(LocatableComponent locatable)
        {
            // Create the icon for the locatable and add a new entry to the dictionary
            if(locatable != null && !locatableIconDictionary.ContainsKey(locatable))
            {
                var icon = locatable.CreateIcon();

                icon.transform.SetParent(iconContainer.transform, false);

                locatableIconDictionary.Add(locatable, icon);

                _rectTransforms.Add(icon, icon.GetComponent<RectTransform>());
            }
        }

        /// <summary>
        /// Callback invoked when a locatable has been removed
        /// </summary>
        /// <param name="locatable"></param>
        private void OnLocatableRemoved(LocatableComponent locatable)
        {
            // Remove the locatable from the dictionary and destroy the icon
            if (locatable != null && locatableIconDictionary.TryGetValue(locatable, out LocatableIconComponent icon))
            {
                locatableIconDictionary.Remove(locatable);

                _rectTransforms.Remove(icon);

                Destroy(icon.gameObject);
            }
        }

        private void Update()
        {
            if (Player != null)
            {
                UpdateLocatableIcons();
            }
        }

        /// <summary>
        /// Updates the position of all icons
        /// </summary>
        private void UpdateLocatableIcons()
        {
            // Run through all locatables in the dictionary
            foreach (var locatable in locatableIconDictionary.Keys)
            {
                // Update the icon position and visibility for the locatable
                if (locatableIconDictionary.TryGetValue(locatable, out var icon))
                {
                    if (TryGetIconLocation(locatable, out var iconLocation))
                    {
                        icon.SetVisible(true);

                        //var rectTransform = icon.GetComponent<RectTransform>();
                        _rectTransforms.TryGetValue(icon, out var rectTransform);

                        rectTransform.anchoredPosition = iconLocation;
                    }
                    else
                    {
                        icon.SetVisible(false);
                    }
                }
            }
        }

        /// <summary>
        /// Computes the location of the icon on the radar. Returns true if the icon is visible, false otherwise
        /// </summary>
        /// <param name="locatable"></param>
        /// <param name="iconLocation"></param>
        /// <returns></returns>
        private bool TryGetIconLocation(LocatableComponent locatable, out Vector2 iconLocation)
        {
            iconLocation = GetDistanceToPlayer(locatable);

            float radarSize = GetRadarUISize();

            var scale = radarSize / Range;

            iconLocation *= scale;

            // Rotate the icon by the players y rotation if enabled
            if(ApplyRotation)
            {
                // Get the forward vector of the player projected on the xz plane
                var playerForwardDirectionXZ = Vector3.ProjectOnPlane(Player.transform.forward, Vector3.up);

                // Create a rotation from the direction
                var rotation = Quaternion.LookRotation(playerForwardDirectionXZ);

                // Mirror y rotation
                var euler = rotation.eulerAngles;
                euler.y = -euler.y;
                rotation.eulerAngles = euler;

                // Rotate the icon location in 3D space
                var rotatedIconLocation = rotation * new Vector3(iconLocation.x, 0.0f, iconLocation.y);

                // Convert from 3D to 2D
                iconLocation = new Vector2(rotatedIconLocation.x, rotatedIconLocation.z);
            }

            if (iconLocation.sqrMagnitude < radarSize*radarSize || locatable.ClampOnRadar)
            {
                // Make sure it is not shown outside the radar
                iconLocation = Vector2.ClampMagnitude(iconLocation, radarSize);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the size of the radar UI
        /// </summary>
        /// <returns></returns>
        private float GetRadarUISize()
        {
            return iconContainer.rect.width / 2;
        }

        /// <summary>
        /// Returns the distance to the player on the x,z plane
        /// </summary>
        /// <param name="locatable"></param>
        /// <returns></returns>
        private Vector2 GetDistanceToPlayer(LocatableComponent locatable)
        {
            Vector3 distanceToPlayer = locatable.transform.position - Player.transform.position;

            return new Vector2(distanceToPlayer.x, distanceToPlayer.z);
        }
    }
}