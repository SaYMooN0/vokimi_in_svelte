<script lang="ts">
    export let closeDialog: () => void;
</script>

<div>
    Link to change password has been sent to your email. Click on the link in
    your email client to change password
</div>
<button class="submit-btn" on:click={closeDialog}>Close</button>
