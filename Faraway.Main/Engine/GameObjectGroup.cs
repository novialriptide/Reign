using Faraway.Main.Engine.Components;
using System.Collections.Generic;

namespace Faraway.Main.Engine
{
    /// <summary>
    /// Mainly used to sort through GameObjects.
    /// </summary>
    public class GameObjectGroup
    {
        GameObject[] cachedGameObjects;
        /// <summary>
        /// The result of this function is cached once used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public GameObject[] Match<T>(GameObject[] gameObjects) where T : Component
        {
            var gameObjects = new List<GameObject>();

            foreach (var gameObject in gameObjects)
                if (gameObject.ContainsComponent<T>())
                    gameObjects.Add(gameObject);

            cachedGameObjects = gameObjects.ToArray();

            return cachedGameObjects;
        }
    }
}
