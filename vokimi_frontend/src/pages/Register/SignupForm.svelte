<script lang="ts">
    let email: string = "";
    let password: string = "";
    let username: string = "";
    let message: string = "";
    let valid: boolean = false;

    export let ChangeStateToLinkHasBeenSentMessage: (
        usernameVal: string,
        emailVal: string,
    ) => void;

    function validateEmail(email: string) {
        const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        return regex.test(email);
    }

    function validateForm() {
        if (!email || !password) {
            message = "Please fill in all the fields.";
            valid = false;
        } else if (!validateEmail(email)) {
            message = "Please enter a valid email address.";
            valid = false;
        } else {
            message = "";
            valid = true;
        }
    }

    async function submitForm() {
        validateForm();
        if (valid) {
            const data = { email, password, username };
            const response = await fetch("/signup", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });

            if (response.ok) {
                ChangeStateToLinkHasBeenSentMessage(username, email);
            } else if (response.status === 400) {
                const errorResponse = await response.json();
                message = errorResponse.Error || "An unknown error occurred.";
            } else {
                message = "An unknown error occurred.";
            }
        }
    }
</script>

<form class="signup-form" on:submit|preventDefault={submitForm}>
    <label for="email-input">Your Email:</label>
    <input
        id="email-input"
        type="email"
        placeholder="Enter your email"
        bind:value={email}
        on:input={validateForm}
    />
    <label for="password-input">Your Password:</label>
    <input
        id="password-input"
        type="password"
        placeholder="Enter your password"
        bind:value={password}
        on:input={validateForm}
    />

    <label for="username-input">Your Username:</label>
    <input
        id="username-input"
        type="text"
        placeholder="Enter your username"
        bind:value={username}
        on:input={validateForm}
    />

    <button type="submit">Register</button>

    <p>{message}</p>
    <a href="/login">I already have an account</a>
</form>
