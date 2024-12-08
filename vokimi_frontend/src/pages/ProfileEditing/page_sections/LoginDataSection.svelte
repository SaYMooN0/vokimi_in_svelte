<script lang="ts">
    import SectionHeader from "../section_shared_components/SectionHeader.svelte";

    export let email: string = "";
</script>

<SectionHeader headerText="Login data" />
<div class="login-data-section">
    <p>
        <span>Account email:</span>
        <span>{email}</span>
    </p>
    <button class="update-password-btn">Update password</button>
</div>

<style>
    .login-data-section {
        display: flex;
        flex-direction: column;
    }
    .update-password-btn {
        margin-bottom: 20px;
        width: fit-content;
        padding: 8px 24px;
        background: var(--primary);
        border-radius: 4px;
        outline: none;
        border: none;
        color: var(--back-main);
        font-size: 18px;
        cursor: pointer;
        transition: all 0.12s ease;
    }
    .update-password-btn:hover {
        transform: scale(1.02);
        background-color: var(--primary-hov);
    }
</style>
