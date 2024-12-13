<script lang="ts">
    import { onMount } from "svelte";
    export let userId: string;
    export let updateParentElement: (
        viewerFollowsUser: boolean,
        userFollowsViewer: boolean,
    ) => void;
    async function unfollowBtnClicked() {}
    function manageNotificationsBtnClicked() {}
    let isMenuOpen = false;
    let buttonElement: any;

    function toggleMenu() {
        isMenuOpen = !isMenuOpen;
    }

    function handleClickOutside(event: any) {
        if (!buttonElement.contains(event.target)) {
            isMenuOpen = false;
        }
    }

    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });
</script>

<div bind:this={buttonElement} class="container unselectable">
    <button class="followed-btn" on:click={toggleMenu}>
        <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M14 18C14 18 15 18 16 20C16 20 19.1765 15 22 14"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M13 22H6.59087C5.04549 22 3.81631 21.248 2.71266 20.1966C0.453365 18.0441 4.1628 16.324 5.57757 15.4816C8.75591 13.5891 12.7529 13.5096 16 15.2432"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z"
                stroke="currentColor"
                stroke-width="1.5"
            />
        </svg>
        Followed
    </button>
    <div class="followed-context-menu" class:open={isMenuOpen}>
        <button
            class="manage-notifications-btn"
            on:click={manageNotificationsBtnClicked}
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M2.52992 14.7696C2.31727 16.1636 3.268 17.1312 4.43205 17.6134C8.89481 19.4622 15.1052 19.4622 19.5679 17.6134C20.732 17.1312 21.6827 16.1636 21.4701 14.7696C21.3394 13.9129 20.6932 13.1995 20.2144 12.5029C19.5873 11.5793 19.525 10.5718 19.5249 9.5C19.5249 5.35786 16.1559 2 12 2C7.84413 2 4.47513 5.35786 4.47513 9.5C4.47503 10.5718 4.41272 11.5793 3.78561 12.5029C3.30684 13.1995 2.66061 13.9129 2.52992 14.7696Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M8 19C8.45849 20.7252 10.0755 22 12 22C13.9245 22 15.5415 20.7252 16 19"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Manage notifications
        </button>
        <button class="unfollow-btn" on:click={unfollowBtnClicked}>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M13 22H6.59087C5.04549 22 3.81631 21.248 2.71266 20.1966C0.453365 18.0441 4.1628 16.324 5.57757 15.4816C7.97679 14.053 10.8425 13.6575 13.5 14.2952"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                />
                <path
                    d="M16 22L19 19M19 19L22 16M19 19L16 16M19 19L22 22"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                />
            </svg>
            Unfollow
        </button>
    </div>
</div>

<style>
    .container {
        position: relative;
        display: inline-block;
    }
    .followed-btn {
        position: relative;
        cursor: pointer;
        user-select: none;
    }

    .followed-context-menu {
        position: absolute;
        width: 280px;
        top: calc(100% + 5px);
        left: 50%;
        transform: translateX(-50%);
        background: var(--back-main);
        z-index: 1000;
        display: none;
        gap: 4px;
        box-shadow: rgba(102, 90, 118, 0.12) 0px 2px 6px 0px;
        padding: 4px;
        box-sizing: border-box;
        border-radius: 4px;
    }

    .followed-context-menu.open {
        display: flex;
        flex-direction: column;
    }
    .followed-context-menu button {
        width: 100%;
        background-color: var(--back-main);
        grid-template-columns: 28px 1fr;
        color: var(--text-faded);
        display: grid;
        align-items: center;
        text-align: left;
        gap: 12px;
    }
    .followed-context-menu button > svg {
        color: inherit;
        width: 100%;
        aspect-ratio: 1/1;
    }
    .followed-context-menu button:hover {
        background-color: var(--back-secondary);
    }
    .followed-context-menu button:active {
        transform: scale(0.98);
    }
</style>
