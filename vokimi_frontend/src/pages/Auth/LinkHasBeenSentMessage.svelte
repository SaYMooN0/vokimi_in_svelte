<script lang="ts">
    export let username: string = "";
    export let email: string = "";
</script>

<h3 class="thank-you">{username}, thank you for registration</h3>
<p>
    Link to complete the registration has been sent to <span>{email}</span>.
    Click on the link in your email client to complete the registration
</p>
