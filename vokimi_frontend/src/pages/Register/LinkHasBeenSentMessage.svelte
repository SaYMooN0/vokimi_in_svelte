<script lang="ts">
    let username: string = "";
    let email: string = "";
    export const SetUsernameAndEmail = function (
        usernameVal: string,
        emailVal: string,
    ) {
        username = usernameVal;
        email = emailVal;
    };
</script>

<h3 class="thank-you">{username}, thank you for registration</h3>
<p>
    Link to complete the registration has been sent to <span>{email}</span>.
    Click on the link in your email client to complete the registration
</p>
