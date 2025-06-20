#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MalbersAnimations.Controller
{
    public class MenuCreateRider
    {
        private readonly static string HumanPlayerPrefab = "Assets/Malbers Animations/Horse AnimSet Pro/4 - Prefabs/Rider/Rider Base.prefab";

        [MenuItem("GameObject/Malbers Animations/Create Rider Player", false, -100)]
        static void CreatePlayer(MenuCommand menuCommand)
        {
            var gameObject = menuCommand.context as GameObject;

            if (gameObject != null)
            {
                DoHuman(gameObject, HumanPlayerPrefab);
                Debug.Log("Rider Player Created!. Please save your the new created Rider as a Prefab [Variant] on your project", gameObject);
            }
        }


        private static void DoHuman(GameObject gameObject, string path)
        {
            gameObject.transform.ResetLocal(); //important to reset the transform Local

            if (!gameObject.TryGetComponent<Animator>(out var animator))
                animator = gameObject.AddComponent<Animator>();

            var currentAvatar = animator.avatar;
            var AvatarRoot = animator.avatarRoot;
            var humanPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var sceneObj = (GameObject)PrefabUtility.InstantiatePrefab(humanPrefab);
            sceneObj.GetComponent<Animator>().avatar = currentAvatar; //Set the Avatar to the new Player

            gameObject.transform.parent = sceneObj.transform;
            gameObject.transform.ResetLocal();
            sceneObj.transform.ResetLocal();

            var animal = sceneObj.GetComponent<MAnimal>();
            animal.RootBone = AvatarRoot; //Set the Root Bone on the Animal Controller
            sceneObj.name = gameObject.name;

            Selection.activeGameObject = sceneObj; //Select the new Player
            GameObject.DestroyImmediate(animator); //Remove the animator
        }
    }
}
#endif