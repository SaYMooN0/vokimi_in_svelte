<script lang="ts">
    export let confirmationString: string;
    export let userId: string;

    let errorMessage: string = "";

    async function TryConfirmRegistration(): Promise<boolean> {
        const data = { confirmationString, userId };
        const response = await fetch("/api/confirmRegistration", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (response.ok) {
            return true;
        } else if (response.status === 400) {
            const errorResponse = await response.json();
            errorMessage = errorResponse.error || "An unknown error occurred.";
            return false;
        } else {
            errorMessage = "An unknown error occurred.";
            return false;
        }
    }
</script>

{#await TryConfirmRegistration()}
    <div>Waiting for server response...</div>
{:then isConfirmed}
    {#if isConfirmed}
        <div>Registration successfully confirmed</div>
    {:else}
        <div>{errorMessage}</div>
    {/if}
{/await}
