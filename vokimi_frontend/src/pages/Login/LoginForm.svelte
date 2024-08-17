<script lang="ts">
    let email = "";
    let password = "";
    let message = "";

    async function submitForm() {
        const data = { email, password };
        console.log(JSON.stringify(data));
        const response = await fetch("/api/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (response.ok) {
            window.location.href = "/";
        } else {
            message = "Error Logging in.";
        }
    }
</script>

<form on:submit|preventDefault={submitForm}>
    <p>
        Email: <input
            type="email"
            placeholder="Enter your email"
            bind:value={email}
        />
    </p>
    <p>
        Password:
        <input
            type="password"
            placeholder="Enter your password"
            bind:value={password}
        />
    </p>

    <button class="login-button" type="submit">Login</button>
    <p>{message}</p>
</form>
<a href="/register">Register</a>

<style>
    .login-button {
        background-color: var(--primary);
        color: var(--back-main);
        outline: none;
        border: none;
        border-radius: 30px;
        padding: 8px 28px;
        font-size: 24px;
    }
</style>
