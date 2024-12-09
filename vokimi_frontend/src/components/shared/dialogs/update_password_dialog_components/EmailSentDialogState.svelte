<script lang="ts">
    export let closeDialog: () => void;
</script>

<div>
    Link to change password has been sent to your email. Click on the link in
    your email client to change password
</div>
<div class="email-sent-close-btn" on:click={closeDialog}>Close</div>

<style>
    .email-sent-close-btn {
        color: var(--primary);
    }
</style>
