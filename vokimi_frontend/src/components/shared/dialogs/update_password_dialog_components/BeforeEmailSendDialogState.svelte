<script lang="ts">
    export let email: string;
    export let changeStateToEmailEntering: () => void;
    export let sendLink: (email: string) => Promise<void>;
</script>

<p>
    You want to change password for:
    <span>{email}</span>
</p>
<span class="change-email-link" on:click={changeStateToEmailEntering}>
    I want to update password for another email
</span>
<button class="continue-btn" on:click={() => sendLink(email)}>
    Continue
</button>

<style>
    .change-email-link {
        color: var(--text-faded);
        padding: 2px 4px;
        border-radius: 2px;
    }
    .change-email-link:hover {
        color: var(--primary);
    }
    .change-email-link:active {
        background-color: var(--back-secondary);
        color: var(--primary-hov);
    }
</style>
