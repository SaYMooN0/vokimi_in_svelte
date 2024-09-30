<script lang="ts">
    import NavMenu from "./NavMenu.svelte";
    import vokimiLogo from "../../../assets/vokimi-logo.svg";

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

<div class="header-layout">
    <a href="/" class="logo-container">
        <img src={vokimiLogo} class="vokimi-logo" alt="Vokimi Logo" />
    </a>

    <NavMenu />

    {#await IsAuthenticated()}
        <div></div>
    {:then authenticated}
        {#if authenticated}
            <div class="acc-div">
                <img
                    src={profilePicture}
                    alt="Profile Picture"
                    class="acc-img"
                />
                {username}
            </div>
        {:else}
            <a href="/auth/login" class="login-button">Login</a>
        {/if}
    {/await}
</div>

<style>
    .header-layout {
        position: fixed;
        top: 0;
        width: 100vw;
        padding: 0 20px;
        box-sizing: border-box;
        display: grid;
        grid-template-columns: 200px 1fr 200px;
        align-items: center;
        height: var(--header-height);
        background-color: var(--back-main);
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
        padding: 8px 28px;
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
        height: 62%;
        margin: auto 0;
        padding: 4px 0;
        width: 100%;
        border: 2px solid var(--back-secondary);
        border-radius: 100px;
        display: flex;
        align-items: center;
        gap: 8px;
        cursor: pointer;
    }
    .acc-div:hover {
        border: 2px solid var(--primary);
    }
</style>
