<script lang="ts">
    export let email: string;
    export let changeStateToEmailEntering: () => void;
    export let sendLink: (email: string) => Promise<void>;
</script>

<p class="before-email-send-p">
    You want to change password for:
    <span>{email}</span>
</p>
<span class="dialog-link" on:click={changeStateToEmailEntering}>
    I want to update password for another email
</span>
<button class="submit-btn" on:click={() => sendLink(email)}> Continue </button>

<style>
    .before-email-send-p {
        text-align: center;
        font-size: 26px;
    }

</style>
