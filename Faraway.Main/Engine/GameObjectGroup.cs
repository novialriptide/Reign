using Faraway.Main.Engine.Components;
using System.Collections.Generic;

namespace Faraway.Main.Engine
{
    /// <summary>
    /// To utilize this object, please use <c>Scene.CreateGameObjectGroup()</c>.
    /// </summary>
    public class GameObjectGroup
    {
        public Scene Scene;
        GameObject[] cachedGameObjects;
        public GameObjectGroup(Scene scene)
        {
            this.Scene = scene;
        }
        /// <summary>
        /// The result of this function is cached once used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public GameObject[] Match<T>() where T : Component
        {
            var gameObjects = new List<GameObject>();

            foreach (var gameObject in Scene.GameObjects)
                if (gameObject.ContainsComponent<T>())
                    gameObjects.Add(gameObject);

            cachedGameObjects = gameObjects.ToArray();

            return cachedGameObjects;
        }
    }
}
