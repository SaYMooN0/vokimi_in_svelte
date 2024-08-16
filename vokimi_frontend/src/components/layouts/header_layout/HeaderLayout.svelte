<script lang="ts">
    import NavMenu from "./NavMenu.svelte";
    import { Link } from "svelte-routing";
    import vokimiLogo from "../../../assets/vokimi-logo.svg";

    let username: string = "";
    let profilePicture: string = "";

    async function IsAuthenticated(): Promise<boolean> {
        const response = await fetch("/api/getUsernameWithProfilePicture");
        console.log(response);

        if (response.status == 200) {
            //#TODO: create object
            console.log("status 200");
            let j = await response.json();
            console.log("-----");
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
        <a href="/login" class="login-button">Login</a>
    {:then authenticated}
        {#if authenticated}
            <div>{username}</div>
        {:else}
            <a href="/login" class="login-button">Login</a>
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
    .login-button:hover{
        background-color: var(--primary-hov);
    }
</style>
