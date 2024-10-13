<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
</script>

<AuthorizeView>
    <div slot="loading">
        <span>Checking Authentication</span>
    </div>
    <div slot="authenticated">Profile editing page</div>
    <div slot="unauthenticated" class="auth-needed">
        You need to log in into your account to edit profile
    </div>
</AuthorizeView>
