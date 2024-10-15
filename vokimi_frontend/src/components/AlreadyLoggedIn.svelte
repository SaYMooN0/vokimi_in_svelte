<script lang="ts">
    async function logout() {
        const response = await fetch("/api/logout", { method: "POST" });
        if (response.ok) {
            window.location.href = "/login";
        }
    }
</script>

<div class="already-logged-in-container">
    <h2>You are already logged in</h2>
    <a href="/acc" class="acc-link">My Account</a>
    <label class="logout-btn" on:click={logout}>Log Out</label>
</div>

<style>
</style>
