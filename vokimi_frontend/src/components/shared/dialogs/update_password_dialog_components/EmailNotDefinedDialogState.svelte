<script lang="ts">
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let sendLink: (email: string) => Promise<void>;
    async function confirmEmailInput() {
        if (StringUtils.isNullOrWhiteSpace(emailInput)) {
            emailEnteringErr = "Please, enter your email";
        } else if (!emailInput.includes("@")) {
            emailEnteringErr = "Please, enter a valid email";
        } else {
            emailEnteringErr = "";
            await sendLink(emailInput);
        }
    }
    let emailInput: string;
    let emailEnteringErr: string = "";
</script>

<p class="enter-email-p">Please, enter your email which you used to register</p>
<a href="/auth/login" class="no-account-link">I don't have an account yet</a>
<input type="email" bind:value={emailInput} />
<p class="error-message">{emailEnteringErr}</p>
<button class="update-password-btn" on:click={() => confirmEmailInput()}>
    Send Password Update Link
</button>

<style>
    .enter-email-p {
        color: var(--text);
        font-size: 28px;
    }
    .no-account-link {
        color: var(--text-faded);
        padding: 2px 4px;
        border-radius: 2px;
    }
    .no-account-link:hover {
        color: var(--primary);
    }
    .no-account-link:active {
        background-color: var(--back-secondary);
        color: var(--primary-hov);
    }
    .error-message {
        color: var(--red-del);
        font-size: 14px;
        font-weight: 500;
    }
    .update-password-btn {
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
    .update-password-btn:hover {
        background-color: var(--primary-hov);
    }
</style>
