<script lang="ts">
    import { onMount, onDestroy } from "svelte";
    import { navigate } from "svelte-routing";
    import ActionConfirmationDialog from "../../../../components/shared/ActionConfirmationDialog.svelte";
    import { Err } from "../../../../ts/Err";

    let currentTestId: string;
    let menuPosition = { top: 0, left: 0 };
    let isMenuOpen = false;

    export function open(testId: string, event: MouseEvent) {
        currentTestId = testId;

        const buttonRect = (
            event.currentTarget as HTMLElement
        ).getBoundingClientRect();
        menuPosition = {
            top: buttonRect.top + window.scrollY,
            left: buttonRect.left,
        };

        isMenuOpen = true;
    }

    function ToEditingAction() {
        navigate(`/testCreation/${currentTestId}/main-info-view`);
        closeMenu();
    }

    function PublishAction() {
        navigate(`/testCreation/${currentTestId}/view-publish`);
        closeMenu();
    }

    function DeleteAction() {
        const testDeletingAction = async () => {
            return Err.none();
        };
        actionConfirmationDialog.open(
            testDeletingAction,
            "Do you really want to delete this draft test?",
        );
        closeMenu();
    }

    function closeMenu() {
        isMenuOpen = false;
    }

    function handleClickOutside(event: MouseEvent) {
        const menu = document.querySelector(".actions-menu");
        if (menu && !menu.contains(event.target as Node)) {
            closeMenu();
        }
    }

    function handleScroll() {
        closeMenu();
    }

    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        document.addEventListener("scroll", handleScroll);
    });

    onDestroy(() => {
        document.removeEventListener("click", handleClickOutside);
        document.removeEventListener("scroll", handleScroll);
    });

    let actionConfirmationDialog: ActionConfirmationDialog;
</script>

<ActionConfirmationDialog bind:this={actionConfirmationDialog} />
<div
    class="actions-menu unselectable"
    class:open={isMenuOpen}
    style="top: {menuPosition.top}px; left: calc({menuPosition.left}px - var(--menu-width) - 10px)"
>
    <div class="action-item" on:click={ToEditingAction}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M20.302 4.25276C21.5 5.6554 21.5 7.77027 21.5 12C21.5 16.2297 21.5 18.3446 20.302 19.7472C20.1319 19.9464 19.9464 20.1319 19.7472 20.302C18.3446 21.5 16.2297 21.5 12 21.5C7.77027 21.5 5.6554 21.5 4.25276 20.302C4.05358 20.1319 3.86808 19.9464 3.69797 19.7472C2.5 18.3446 2.5 16.2297 2.5 12C2.5 7.77027 2.5 5.6554 3.69797 4.25276C3.86808 4.05358 4.05358 3.86808 4.25276 3.69797C5.6554 2.5 7.77027 2.5 12 2.5C16.2297 2.5 18.3446 2.5 19.7472 3.69797C19.9464 3.86808 20.1319 4.05358 20.302 4.25276Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M15 9L8 16"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
            <path
                d="M10 8.11274C10 8.11274 14.8288 7.70569 15.5615 8.43847C16.2943 9.17125 15.8872 14 15.8872 14"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        To Editing
    </div>
    <div class="action-item" on:click={PublishAction}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M18 7C18.7745 7.16058 19.3588 7.42859 19.8284 7.87589C21 8.99181 21 10.7879 21 14.38C21 17.9721 21 19.7681 19.8284 20.8841C18.6569 22 16.7712 22 13 22H11C7.22876 22 5.34315 22 4.17157 20.8841C3 19.7681 3 17.9721 3 14.38C3 10.7879 3 8.99181 4.17157 7.87589C4.64118 7.42859 5.2255 7.16058 6 7"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linecap="round"
            />
            <path
                d="M12.0253 2.00052L12 14M12.0253 2.00052C11.8627 1.99379 11.6991 2.05191 11.5533 2.17492C10.6469 2.94006 9 4.92886 9 4.92886M12.0253 2.00052C12.1711 2.00657 12.3162 2.06476 12.4468 2.17508C13.3531 2.94037 15 4.92886 15 4.92886"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>

        Publish
    </div>
    <div class="action-item" on:click={DeleteAction}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M19.5 5.5L18.8803 15.5251C18.7219 18.0864 18.6428 19.3671 18.0008 20.2879C17.6833 20.7431 17.2747 21.1273 16.8007 21.416C15.8421 22 14.559 22 11.9927 22C9.42312 22 8.1383 22 7.17905 21.4149C6.7048 21.1257 6.296 20.7408 5.97868 20.2848C5.33688 19.3626 5.25945 18.0801 5.10461 15.5152L4.5 5.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
            <path
                d="M3 5.5H21M16.0557 5.5L15.3731 4.09173C14.9196 3.15626 14.6928 2.68852 14.3017 2.39681C14.215 2.3321 14.1231 2.27454 14.027 2.2247C13.5939 2 13.0741 2 12.0345 2C10.9688 2 10.436 2 9.99568 2.23412C9.8981 2.28601 9.80498 2.3459 9.71729 2.41317C9.32164 2.7167 9.10063 3.20155 8.65861 4.17126L8.05292 5.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
            <path
                d="M9.5 16.5L9.5 10.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
            <path
                d="M14.5 16.5L14.5 10.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
        </svg>
        Delete
    </div>
</div>

<style>
    .actions-menu {
        position: absolute;
        z-index: 100;
        display: none;
        flex-direction: column;
        background-color: var(--back-main);
        padding: 4px;
        border-radius: 6px;
        box-shadow: rgba(0, 0, 0, 0.2) 0px 3px 8px;
        --menu-width: 160px;
        width: var(--menu-width);
    }
    .actions-menu.open {
        display: flex;
    }

    .action-item {
        font-size: 18px;
        padding: 4px 4px;
        border-radius: 4px;
        transition: all 0.08s;
        display: flex;
        align-items: center;
        gap: 4px;
        color: var(--text);
        cursor: default;
    }
    .action-item svg {
        height: 24px;
    }
    .action-item:hover {
        background-color: var(--back-secondary);
        color: var(--primary);
    }
    .action-item:active {
        padding-left: 12px;
    }
</style>
