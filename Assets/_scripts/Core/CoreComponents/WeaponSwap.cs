using System;
using Tero.Interaction;
using Tero.Weapons;
using Tero.Interaction.Interactables;

namespace Tero.CoreSystem
{
    public class WeaponSwap : CoreComponent
    {
        public event Action<WeaponSwapChoiceRequest> OnChoiceRequested;
        public event Action<WeaponDataSO> OnWeaponDiscarded;

        private InteractableDetector interactableDetector;
        private WeaponInventory weaponInventory;

        private WeaponDataSO newWeaponData;

        private WeaponPickup weaponPickup;

        private void HandleTryInteract(IInteractable interactable)
        {
            if (interactable is not WeaponPickup pickup)
                return;

            weaponPickup = pickup;

            newWeaponData = weaponPickup.GetContext();

            if (weaponInventory.TryGetEmptyIndex(out var index))
            {
                weaponInventory.TrySetWeapon(newWeaponData, index, out _);
                interactable.Interact();
                newWeaponData = null;
                return;
            }

            OnChoiceRequested?.Invoke(new WeaponSwapChoiceRequest(
                HandleWeaponSwapChoice,
                weaponInventory.GetWeaponSwapChoices(),
                newWeaponData
            ));
        }

        private void HandleWeaponSwapChoice(WeaponSwapChoice choice)
        {
            if (!weaponInventory.TrySetWeapon(newWeaponData, choice.Index, out var oldData))
                return;

            newWeaponData = null;

            OnWeaponDiscarded?.Invoke(oldData);

            if (weaponPickup is null)
                return;

            weaponPickup.Interact();

        }

        protected override void Awake()
        {
            base.Awake();

            interactableDetector = core.getCoreComponents<InteractableDetector>();
            weaponInventory = core.getCoreComponents<WeaponInventory>();
        }

        private void OnEnable()
        {
            interactableDetector.OnTryInteract += HandleTryInteract;
        }


        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryInteract;
        }
    }
}