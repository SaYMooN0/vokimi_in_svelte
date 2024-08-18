<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import LoginForm from "./LoginForm.svelte";
    import AlreadyLoggedIn from "../../components/AlreadyLoggedIn.svelte";
</script>



<AuthorizeView>
    <div slot="loading">
        <span>Checking Authentication</span>
    </div>
    <div slot="authenticated">
        <AlreadyLoggedIn />
    </div>
    <div slot="unauthenticated">
        <LoginForm />
    </div>
</AuthorizeView>
