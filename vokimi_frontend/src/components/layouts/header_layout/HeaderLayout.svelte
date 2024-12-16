<script lang="ts">
    import NavMenu from "./NavMenu.svelte";
    import vokimiLogo from "../../../assets/vokimi-logo.svg";
    import HeaderAccountDiv from "./HeaderAccountDiv.svelte";

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
            <HeaderAccountDiv {username} {profilePicture} />
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
        grid-template-columns: 200px 1fr 80px;
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

    @media (max-width: 1600px) {
        .vokimi-logo {
            height: 96%;
        }
        .header-layout {
            grid-template-columns: 184px 1fr 80px;
        }
    }
    @media (max-width: 1200px) {
        .vokimi-logo {
            height: 92%;
        }
        .header-layout {
            grid-template-columns: 148px 1fr 80px;
        }
    }
</style>
