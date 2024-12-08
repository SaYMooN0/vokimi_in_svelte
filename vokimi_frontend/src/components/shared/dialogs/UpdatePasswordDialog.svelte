<script lang="ts">
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import BaseDialog from "../../BaseDialog.svelte";
    import LoadingMessage from "../LoadingMessage.svelte";

    let email: string = "";
    let linkHasBeenSent: boolean = false;
    let emailEntering: string = "";
    let linkSendingErr: string = "";
    async function tryGetEmail() {
        const response = await fetch("/api/auth/ping");

        if (response.status === 200) {
            const data = await response.json();
            email = data.email;
            if (!StringUtils.isNullOrWhiteSpace(email)) {
                linkHasBeenSent = true;
            }
        } else {
            email = "";
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
        } else if (response.status === 404) {
            linkHasBeenSent = false;
            linkSendingErr = await getErrorFromResponse(response);
        } else {
            linkHasBeenSent = false;
            linkSendingErr = "Unable to send update password link";
        }
    }
    let dialogElement: BaseDialog;
    let isOpen = false;
</script>

<BaseDialog dialogId="updatePasswordDialog" bind:this={dialogElement}>
    {#if isOpen}
        \
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
                                . Click on the link in your email client to change
                                password
                            </div>
                        {:else}
                            <p class="error-message">{linkSendingErr}</p>
                        {/if}
                    {/await}
                {:else}
                    <div class="email-neeeded">
                        <p>
                            Please, enter your email which you used to register
                        </p>
                        <input type="email" bind:value={emailEntering} />
                        <button on:click={() => (email = emailEntering)}>
                            Confirm
                        </button>
                    </div>
                {/if}
            {/await}
        </div>
    {/if}
</BaseDialog>
