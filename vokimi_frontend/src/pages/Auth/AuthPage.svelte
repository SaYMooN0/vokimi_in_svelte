<script lang="ts">
    import SignupForm from "./SignupForm.svelte";
    import LoginForm from "./LoginForm.svelte";
    import AuthorizeView from "../../components/AuthorizeView.svelte";

    export let path: string;
</script>

<AuthorizeView>
    <div slot="loading">
        <span>Checking Authentication</span>
    </div>
    <div slot="authenticated">
        <AlreadyLoggedIn />
    </div>
    <div slot="unauthenticated">
        <div class="align-center">
            {#if path == "signup"}
                <SignupForm />
            {:else}
                <LoginForm />
            {/if}
        </div>
    </div>
</AuthorizeView>

<style>
    .align-center {
        position: absolute;
        top: 52%;
        left: 50%;
        transform: translate(-50%, -50%);
    }
</style>
