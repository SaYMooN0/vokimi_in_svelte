<script lang="ts">
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import BaseDialog from "../../BaseDialog.svelte";
    import CloseButton from "../CloseButton.svelte";
    import BeforeEmailSendDialogState from "./update_password_dialog_components/BeforeEmailSendDialogState.svelte";
    import EmailNotDefinedDialogState from "./update_password_dialog_components/EmailNotDefinedDialogState.svelte";
    import EmailSentDialogState from "./update_password_dialog_components/EmailSentDialogState.svelte";

    let linkHasBeenSent: boolean;
    let dialogElement: BaseDialog;
    let emailSpecified: boolean;
    let linkSendingErr: string;
    let isOpen = false;

    export async function open() {
        linkSendingErr = "";
        emailSpecified = false;
        linkHasBeenSent = false;
        isOpen = true;
        dialogElement.open();
    }
    async function tryGetEmail(): Promise<string> {
        let email;
        const response = await fetch("/api/auth/ping");
        if (response.status === 200) {
            const data = await response.json();
            email = data.email ?? "";
        }
        emailSpecified = !StringUtils.isNullOrWhiteSpace(email);
        return email;
    }
    async function trySendUpdatePasswordRequest(email: string): Promise<void> {
        linkHasBeenSent = false;
        linkSendingErr = "";

        const frontendBaseUrl = window.location.origin;
        const data = { email, frontendBaseUrl };
        const response = await fetch("/api/auth/createPasswordUpdateRequest", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (response.status === 200) {
            linkHasBeenSent = true;
        } else if (response.status === 400) {
            linkSendingErr = await getErrorFromResponse(response);
        } else {
            linkSendingErr = "Unable to send update password link";
        }
    }
    function closeDialog() {
        isOpen = false;
        dialogElement.close();
    }
    
</script>

<BaseDialog dialogId="updatePasswordDialog" bind:this={dialogElement}>
    {#if isOpen}
        <div class="dialog-content">
            <CloseButton onClose={() => closeDialog()} />
            {#if linkHasBeenSent}
                <EmailSentDialogState
                    closeDialog={() => {
                        closeDialog();
                    }}
                />
            {:else}
                {#await tryGetEmail() then email}
                    {#if emailSpecified}
                        <BeforeEmailSendDialogState
                            {email}
                            changeStateToEmailEntering={() =>
                                (emailSpecified = false)}
                            sendLink={trySendUpdatePasswordRequest}
                        />
                    {:else}
                        <EmailNotDefinedDialogState
                            sendLink={trySendUpdatePasswordRequest}
                        />
                    {/if}
                    {#if !StringUtils.isNullOrWhiteSpace(linkSendingErr)}
                        <p class="err-message">{linkSendingErr}</p>
                    {/if}
                {/await}
            {/if}
        </div>
    {/if}
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 24px 20px;
        position: relative;
    }
    .dialog-content :global(.err-message) {
        color: var(--red-del);
        font-size: 16px;
        font-weight: 500;
    }
    .dialog-content :global(.dialog-link) {
        margin: 0;
        color: var(--text-faded);
        padding: 2px 8px;
        border-radius: 2px;
        text-decoration: underline;
        cursor: pointer;
    }
    .dialog-content :global(.dialog-link):hover {
        color: var(--primary);
    }
    .dialog-content :global(.dialog-link):active {
        background-color: var(--back-secondary);
        color: var(--primary-hov);
    }
    .dialog-content :global(.submit-btn) {
        margin-top: 12px;
        width: fit-content;
        padding: 8px 24px;
        background: var(--primary);
        border-radius: 4px;
        outline: none;
        border: none;
        color: var(--back-main);
        font-size: 18px;
        cursor: pointer;
        transition: all 0.12s ease;
    }
    .dialog-content :global(.submit-btn):hover {
        background-color: var(--primary-hov);
    }
</style>
