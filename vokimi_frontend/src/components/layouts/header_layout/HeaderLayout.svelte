<script lang="ts">
    import NavMenu from "./NavMenu.svelte";
    import vokimiLogo from "../../../assets/vokimi-logo.svg";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";

    let username: string = "";
    let profilePicture: string = "";

    async function IsAuthenticated(): Promise<boolean> {
        const response = await fetch("/api/getUsernameWithProfilePicture");

        if (response.status == 200) {
            let j = await response.json();
            username = j.username;
            profilePicture = j.profilePicture;
            return username != "" && profilePicture != "";
        }
        return false;
    }
</script>

<div class="header-layout unselectable">
    <a href="/" class="logo-container">
        <img src={vokimiLogo} class="vokimi-logo" alt="Vokimi Logo" />
    </a>

    <NavMenu />

    {#await IsAuthenticated()}
        <div>Loading...</div>
    {:then authenticated}
        {#if authenticated}
            <a href="/user" class="acc-div">
                <img
                    src={ImgUtils.imgUrl(profilePicture)}
                    alt="Profile Picture"
                    class="acc-img"
                />
                {username}
            </a>
        {:else}
            <a href="/auth/login" class="login-button">Login</a>
        {/if}
    {/await}
</div>

<style>
    .header-layout {
        z-index: 100;
        position: fixed;
        top: 0;
        width: 100vw;
        padding: 0 calc(3vw - 22px);
        box-sizing: border-box;
        display: grid;
        grid-template-columns: 200px 1fr 200px;
        align-items: center;
        height: var(--header-height);
        background-color: var(--back-main);
        box-shadow: rgba(99, 99, 99, 0.1) 0px 2px 2px 0px;
        transition: all 0.04s;
    }
    .logo-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100%;
    }
    .vokimi-logo {
        height: 100%;
        max-height: var(--header-height);
        object-fit: contain;
    }
    .login-button {
        text-decoration: none;
        width: fit-content;
        justify-self: center;
        padding: 8px 32px;
        background-color: var(--primary);
        color: var(--back-main);
        font-size: 18px;
        border-radius: 20px;
    }
    .login-button:hover {
        background-color: var(--primary-hov);
    }
    .acc-div {
        background-color: var(--back-secondary);
        padding: 4px 12px;
        width: fit-content;
        max-width: 90%;
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
    }

    .acc-img {
        height: 44px;
        aspect-ratio: 1/1;
        object-fit: cover;
        border-radius: 12px;
    }
    @media (max-width: 1600px) {
        .vokimi-logo {
            height: 96%;
        }
        .header-layout {
            grid-template-columns: 184px 1fr 184px;
        }
    }
    @media (max-width: 1200px) {
        .vokimi-logo {
            height: 92%;
        }
        .header-layout {
            grid-template-columns: 148px 1fr 148px;
        }
        .acc-div {
            font-size: 14px;
            gap: 6px;
        }
        .acc-img {
            height: 38px;
            aspect-ratio: 1/1;
            object-fit: cover;
            border-radius: 10px;
        }
    }
</style>
