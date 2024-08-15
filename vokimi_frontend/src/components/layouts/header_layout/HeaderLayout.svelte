<script lang="ts">
    import NavMenu from "./NavMenu.svelte";
    import { Link } from "svelte-routing";

    let username: string = "";
    let profilePicture: string = "";

    async function IsAuthenticated(): Promise<boolean> {
        const response = await fetch("/getUsernameWithProfilePicture");
        console.log(response);
        let status: number = response.status;

        if (status == 200) {
            //#TODO: create object
            let j = await response.json();
            username = j.username;
            profilePicture = j.profilePicture;
            return username != "" && profilePicture != "";
        }
        return false;
    }
</script>

<div class="header-layout">
    <Link to="/" class="vokimi-logo">
        <!-- <VokimiLogo /> -->
        Vokimi
    </Link>

    <NavMenu />

    {#await IsAuthenticated()}
        <Link to="/login" class="login-button">Login</Link>
    {:then authenticated}
        {#if authenticated}
            <div>{username}</div>
        {:else}
            <Link to="/login" class="login-button">Login</Link>
        {/if}
    {/await}
</div>

<style>
    .header-layout {
        width: 100vw;
        box-sizing: border-box;
        margin: 0;
        display: grid;
        grid-template-columns: 200px 1fr 200px;
        align-items: center;
    }
</style>
