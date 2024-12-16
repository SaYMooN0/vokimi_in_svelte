<script lang="ts">
    import { navigate } from "svelte-routing";
    import { StringUtils } from "../../ts/utils/StringUtils";

    export let requestId: string;
    export let confirmationCode: string;

    async function checkForExistence() {
        const response = await fetch(
            `/api/auth/checkPasswordUpdateRequest/${requestId}/${confirmationCode}`,
        );
        if (response.ok) {
            return true;
        }
        return false;
    }
    async function confirmPasswordUpdateRequest() {
        passwordUpdatingErr = "";
        if (StringUtils.isNullOrWhiteSpace(newPassword)) {
            passwordUpdatingErr = "Please enter a new password";
            return;
        }
        const data = {
            updateRequestId: requestId,
            confirmationCode,
            newPassword,
        };
        const response = await fetch("/api/auth/confirmPasswordUpdateRequest", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (response.ok) {
            navigate("/auth/login");
        } else if (response.status === 400) {
            const errorResponse = await response.json();
            passwordUpdatingErr =
                errorResponse.error || "An unknown error occurred.";
        } else {
            passwordUpdatingErr = "An unknown error occurred.";
        }
    }
    let newPassword: string = "";
    let passwordUpdatingErr: string = "";
</script>

<div>
    {#await checkForExistence()}
        <div>Please wait</div>
    {:then requestExists}
        {#if requestExists}
            <p>Enter new password:</p>
            <input type="password" bind:value={newPassword} />
            {#if !StringUtils.isNullOrWhiteSpace(passwordUpdatingErr)}
                <p class="password-updating-err">{passwordUpdatingErr}</p>
            {/if}
            <button class="submit-btn" on:click={confirmPasswordUpdateRequest}>
                Confirm
            </button>
        {:else}
            <p>Invalid request</p>
            <p>Please ensure that you used the correct link</p>
        {/if}
    {/await}
</div>

<style>
</style>
