<script lang="ts">
    import { navigate } from "svelte-routing";
    import { logout } from "../../../ts/stores/authStore";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";
    import { onMount } from "svelte";

    export let username: string;
    export let profilePicture: string;

    async function logoutAction() {
        await logout();
        toggleMenu();
    }
    function toMyPageAction() {
        navigate("/user");
        toggleMenu();
    }
    function editProfileAction() {
        navigate("/profile-editing");
        toggleMenu();
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
    <div class="open-menu-btn" on:click={toggleMenu}>
        <img
            src={ImgUtils.imgUrl(profilePicture)}
            alt="Profile Picture"
            class="acc-img"
        />
        <svg
            xmlns="http://www.w3.org/2000/svg"
            class="open-menu-icon"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M5.99977 9.00005L11.9998 15L17.9998 9"
                stroke="currentColor"
                stroke-width="2.2"
                stroke-miterlimit="16"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </div>
    <div class="context-menu" class:open={isContextMenuOpen}>
        <img
            src={ImgUtils.imgUrl(profilePicture)}
            alt="Profile Picture"
            class="context-menu-img"
        />
        <span class="username-span">
            {username}
        </span>
        <div class="menu-actions-container">
            <div class="menu-action" on:click={toMyPageAction}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M9.13518 2.49991C6.4689 2.56066 4.91156 2.81447 3.8475 3.87483C2.91622 4.80288 2.60492 6.10747 2.50085 8.19991M14.8665 2.49991C17.5328 2.56066 19.0902 2.81447 20.1542 3.87483C21.0855 4.80288 21.3968 6.10747 21.5009 8.19991M14.8665 21.4999C17.5328 21.4392 19.0902 21.1853 20.1542 20.125C21.0855 19.1969 21.3968 17.8923 21.5009 15.7999M9.13518 21.4999C6.4689 21.4392 4.91156 21.1853 3.8475 20.125C2.91622 19.1969 2.60492 17.8923 2.50085 15.7999"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                    <path
                        d="M8 17C9.83846 14.4046 14.1188 14.263 16 17M14.5 9.5C14.5 10.8807 13.3807 12 12 12C10.6193 12 9.5 10.8807 9.5 9.5C9.5 8.11929 10.6193 7 12 7C13.3807 7 14.5 8.11929 14.5 9.5Z"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                    />
                </svg>
                To my page
            </div>
            <div class="menu-action" on:click={editProfileAction}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M10.5 22H6.59087C5.04549 22 3.81631 21.248 2.71266 20.1966C0.453365 18.0441 4.1628 16.324 5.57757 15.4816C8.12805 13.9629 11.2057 13.6118 14 14.4281"
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
                        d="M18.4332 13.8485C18.7685 13.4851 18.9362 13.3035 19.1143 13.1975C19.5442 12.9418 20.0736 12.9339 20.5107 13.1765C20.6918 13.2771 20.8646 13.4537 21.2103 13.8067C21.5559 14.1598 21.7287 14.3364 21.8272 14.5214C22.0647 14.9679 22.0569 15.5087 21.8066 15.9478C21.7029 16.1298 21.5251 16.3011 21.1694 16.6437L16.9378 20.7194C16.2638 21.3686 15.9268 21.6932 15.5056 21.8577C15.0845 22.0222 14.6214 22.0101 13.6954 21.9859L13.5694 21.9826C13.2875 21.9752 13.1466 21.9715 13.0646 21.8785C12.9827 21.7855 12.9939 21.6419 13.0162 21.3548L13.0284 21.1988C13.0914 20.3906 13.1228 19.9865 13.2807 19.6232C13.4385 19.2599 13.7107 18.965 14.2552 18.375L18.4332 13.8485Z"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linejoin="round"
                    />
                </svg>
                Edit profile
            </div>
            <div class="menu-action" on:click={logoutAction}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M11 3L10.3374 3.23384C7.75867 4.144 6.46928 4.59908 5.73464 5.63742C5 6.67576 5 8.0431 5 10.7778V13.2222C5 15.9569 5 17.3242 5.73464 18.3626C6.46928 19.4009 7.75867 19.856 10.3374 20.7662L11 21"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                    />
                    <path
                        d="M21 12L11 12M21 12C21 11.2998 19.0057 9.99153 18.5 9.5M21 12C21 12.7002 19.0057 14.0085 18.5 14.5"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                </svg>Log out
            </div>
        </div>
    </div>
</div>

<style>
    .acc-div-container {
        position: relative;
        display: inline-block;
    }
    .open-menu-btn {
        width: fit-content;
        position: relative;
        cursor: pointer;
        user-select: none;
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: center;
        padding: 4px 0;
        width: 96px;
        border-radius: 8px;
        gap: 0;
        transition: all 0.1s ease-in;
    }

    .open-menu-icon {
        height: 28px;
        width: 28px;
        color: var(--text-faded);
    }
    .open-menu-btn:hover {
        background-color: var(--back-secondary);
        gap: 4px;
    }
    .open-menu-btn:hover .open-menu-icon {
        color: var(--primary);
        transform: scale(1.04);
    }
    .context-menu {
        position: absolute;
        width: 200px;
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
        align-items: center;
    }
    .acc-img {
        height: 48px;
        aspect-ratio: 1/1;
        object-fit: cover;
        border-radius: 50%;
    }
    .username-span {
        font-size: 18px;
        font-weight: 600;
        max-width: 90%;
        word-break: break-all;
        text-align: center;
    }
    .context-menu-img {
        height: 64px;
        width: 64px;
        border-radius: 50%;
    }
    .menu-actions-container {
        width: 100%;
        display: flex;
        flex-direction: column;
        gap: 2px;
    }
    .menu-action {
        width: 100%;
        box-sizing: border-box;
        padding: 2px 8px;
        font-size: 16px;
        border-radius: 4px;
        cursor: pointer;
        transition: all 0.04s ease-in;
        display: grid;
        grid-template-columns: 24px 1fr;
        align-items: center;
        gap: 4px;
        color: var(--text);
        transition: all 0.1s ease-in;
    }
    .menu-action:hover {
        background-color: var(--back-secondary);
        color: var(--primary);
        gap: 6px;
    }
    .menu-action:active{
        color: var(--primary-hov);
    }
</style>
