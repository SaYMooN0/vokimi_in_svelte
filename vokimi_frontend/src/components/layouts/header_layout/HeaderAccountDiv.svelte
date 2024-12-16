<script lang="ts">
    import { navigate } from "svelte-routing";
    import { logout } from "../../../ts/stores/authStore";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";
    import { onMount } from "svelte";

    export let username: string;
    export let profilePicture: string;

    async function logoutAction() {
        await logout();
    }
    function toMyPageAction() {
        navigate("/user");
    }
    function editProfileAction() {
        navigate("/profile-editing");
    }
    let isContextMenuOpen = false;
    let buttonElement: any;
    function toggleMenu() {
        isContextMenuOpen = !isContextMenuOpen;
    }
    function handleClickOutside(event: any) {
        if (!buttonElement.contains(event.target)) {
            isContextMenuOpen = false;
        }
    }

    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });
</script>

<div bind:this={buttonElement} class="acc-div-container">
    <div class="always-visible" on:click={toggleMenu}>
        <img
            src={ImgUtils.imgUrl(profilePicture)}
            alt="Profile Picture"
            class="acc-img"
        />
    </div>
    <div class="context-menu" class:open={isContextMenuOpen}>
        <span class="username-span">
            {username}
        </span>
        <div class="menu-actions-container">
            <div class="menu-action" on:click={toMyPageAction}>To my page</div>
            <div class="menu-action" on:click={editProfileAction}>
                Edit profile
            </div>
            <div class="menu-action" on:click={logoutAction}>Log out</div>
        </div>
    </div>
</div>

<style>
    .acc-div-container {
        position: relative;
        display: inline-block;
    }
    .always-visible {
        position: relative;
        cursor: pointer;
        user-select: none;
    }

    .context-menu {
        position: absolute;
        width: 200px;
        top: calc(100% + 5px);
        right: 20%;
        background: var(--back-main);
        z-index: 1000;
        display: none;
        gap: 4px;
        box-shadow: rgba(102, 90, 118, 0.12) 0px 2px 6px 0px;
        padding: 4px;
        box-sizing: border-box;
        border-radius: 4px;
    }
    .context-menu.open {
        display: flex;
        flex-direction: column;
    }
    /* .acc-div {
        background-color: var(--back-secondary);
        padding: 4px 12px;
        width: fit-content;
        max-width: 92%;
        overflow: hidden;
        border: 2px solid var(--back-secondary);
        border-radius: 100px;
        display: grid;
        grid-template-columns: auto 1fr;
        align-items: center;
        gap: 8px;
        font-size: 16px;
        cursor: pointer;
    }
    .acc-div:hover {
        border: 2px solid var(--primary);
    }
    .acc-div:active {
        transform: scale(0.98);
    } */
    .acc-img {
        height: 44px;
        aspect-ratio: 1/1;
        object-fit: cover;
        border-radius: 12px;
    }
    .menu-actions-container {
        display: flex;
        flex-direction: column;
    }
</style>
