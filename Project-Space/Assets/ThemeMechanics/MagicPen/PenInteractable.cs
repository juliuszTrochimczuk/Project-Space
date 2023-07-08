using UnityEngine;

namespace ThemeMechanics.MagicPen
{
    /// <summary>
    /// Abstract class for objects that can interact with magic pen
    /// </summary>
    public abstract class PenInteractable : MonoBehaviour
    {
        /// <summary>
        /// Returns true if object exists on scene
        /// </summary>
        public abstract bool ObjectExists { get; }
        public abstract bool CanBeDestroyed { get; }
        public abstract bool CanBeInstantiated { get; }
        public virtual bool InteractionPossible { get => CanBeInstantiated || CanBeDestroyed; }

        public virtual bool TryDestroy()
        {
            if (CanBeDestroyed)
            {
                Destroy();
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool TryInstantiate(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (CanBeInstantiated)
            {
                Instantiate(position, rotation, parent);
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool TryInstantiate(Vector3 position, Quaternion rotation)
        {
            if (CanBeInstantiated)
            {
                Instantiate(position, rotation);
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract void Destroy();
        public abstract void Instantiate(Vector3 position, Quaternion rotation, Transform parent);
        public abstract void Instantiate(Vector3 position, Quaternion rotation);

        /// <summary>
        /// Destorys object if it exists in the scene, otherwise destroys it
        /// </summary>
        /// <param name="position">position in which object will be instantiated if it exists in the scene</param>
        /// <param name="rotation">oriantation in which object will be instantiated if it exists in the scene</param>
        /// <returns>True if operation was succesfull</returns>
        public bool TryInteract(Vector3 position, Quaternion rotation) => ObjectExists ? TryDestroy() : TryInstantiate(position, rotation);
        /// <summary>
        /// Destorys object if it exists in the scene, otherwise destroys it
        /// </summary>
        /// <param name="position">position in which object will be instantiated if it exists in the scene</param>
        /// <param name="rotation">oriantation in which object will be instantiated if it exists in the scene</param>
        /// <param name="parent">transform which will be the parernt of the spawned object</param>
        /// <returns>True if operation was succesfull</returns>
        public bool TryInteract(Vector3 position, Quaternion rotation, Transform parent) => ObjectExists ? TryDestroy() : TryInstantiate(position, rotation, parent);
        /// <summary>
        /// Destorys object if it exists in the scene, otherwise destroys it
        /// </summary>
        /// <param name="position">position in which object will be instantiated if it exists in the scene</param>
        /// <param name="rotation">oriantation in which object will be instantiated if it exists in the scene</param>
        public void Interact(Vector3 position, Quaternion rotation)
        {
            if (ObjectExists) Destroy();
            else Instantiate(position, rotation);
        }
        /// <summary>
        /// Destorys object if it exists in the scene, otherwise destroys it
        /// </summary>
        /// <param name="position">position in which object will be instantiated if it exists in the scene</param>
        /// <param name="rotation">oriantation in which object will be instantiated if it exists in the scene</param>
        /// <param name="parent">transform which will be the parernt of the spawned object</param>
        public void Interact(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (ObjectExists) Destroy();
            else Instantiate(position, rotation, parent);
        }
    }
}
