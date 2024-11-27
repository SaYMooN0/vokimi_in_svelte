<script lang="ts">
    import LoadingMessage from "../../components/shared/LoadingMessage.svelte";
    import { StringUtils } from "../../ts/utils/StringUtils";

    let email: string = "";
    let linkHasBeenSent: boolean = false;
    let emailEntering: string = "";
    async function tryGetEmail() {
        const response = await fetch("/api/auth/ping");

        if (response.status === 200) {
            const data = await response.json();
            email = data.email;
            if (!StringUtils.isNullOrWhiteSpace(email)) {
                linkHasBeenSent = true;
            }
        }
    }
    async function sendUpdatePasswordRequest() {
        const response = await fetch(
            "/api/auth/sendUpdatePasswordRequest/" + email,
            {
                method: "POST",
            },
        );
        if (response.status === 200) {
            linkHasBeenSent = true;
        }
    }
</script>

<div>
    {#await tryGetEmail() then _}
        {#if !StringUtils.isNullOrWhiteSpace(email)}
            {#await sendUpdatePasswordRequest()}
                <LoadingMessage />
            {:then}
                {#if linkHasBeenSent}
                    <div>
                        Link to change password has been sent to
                        <span>{email}</span>
                        . Click on the link in your email client to change password
                    </div>
                {:else}
                    <p class="error-message">
                        An error has occurred. Please try again later
                    </p>
                {/if}
            {/await}
        {:else}
            <div class="email-neeeded">
                <p>Please, enter your email which you used to register</p>
                <input type="email" bind:value={emailEntering} />
                <button on:click={() => (email = emailEntering)}>
                    Confirm
                </button>
            </div>
        {/if}
    {/await}
</div>
