using System.Collections.Generic;
using Faraway.Engine.Components;

namespace Faraway.Engine
{
    /// <summary>
    /// Mainly used to sort through GameObjects.
    /// </summary>
    public sealed class GameObjectGroup
    {
        private GameObject[] oldGameObjects;
        private GameObject[] cachedGameObjects;
        /// <summary>
        /// The result of this function is cached once used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public GameObject[] Match<T>(GameObject[] gameObjects) where T : Component
        {
            if (cachedGameObjects != null)
                return cachedGameObjects;

            if (oldGameObjects == gameObjects)
                return cachedGameObjects;

            oldGameObjects = gameObjects;
            var outGameObjects = new List<GameObject>();

            foreach (var gameObject in gameObjects)
                if (gameObject.ContainsComponent<T>())
                    outGameObjects.Add(gameObject);

            cachedGameObjects = outGameObjects.ToArray();

            return cachedGameObjects;
        }
        public GameObject[] GetChildrenComponents()
        {
            return cachedGameObjects;
        }
    }
}
