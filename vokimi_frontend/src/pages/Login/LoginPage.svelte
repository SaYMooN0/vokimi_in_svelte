<script lang="ts">
    import { PingAuthResponse } from "../../ts/PingAuthResponse";
    import LoginForm from "./LoginForm.svelte";
    import AlreadyLoggedIn from "../../components/AlreadyLoggedIn.svelte";

    async function CheckAuth(): Promise<boolean> {
        const response = await fetch("/api/pingauth");

        if (response.status === 200) {
            const data = await response.json();
            const authResponse = new PingAuthResponse(
                data.Email,
                data.Username,
                data.UserId,
            );
            return authResponse.isAuthenticated();
        } else {
            return false;
        }
    }
</script>

{#await CheckAuth()}
    <span>Checking Authentication</span>
{:then authenticated}
    {#if authenticated}
        <AlreadyLoggedIn />
    {:else}
        <LoginForm />
    {/if}
{/await}
