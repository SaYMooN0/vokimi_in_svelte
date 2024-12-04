<script lang="ts">
    import SectionHeader from "../section_shared_components/SectionHeader.svelte";

    export let email: string = "";
</script>

<SectionHeader headerText="Login data" />
<p class="login-data-p">
    <label class="account-email-label">
        Account email: <span>{email}</span>
    </label>
    <button class="update-password-btn">Update password</button>
</p>

<style>
    .login-data-p {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
</style>
