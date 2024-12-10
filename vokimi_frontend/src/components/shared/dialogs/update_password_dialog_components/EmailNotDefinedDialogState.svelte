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

<p class="enter-email-p unselectable">Please, enter your email which you used to register</p>
<a href="/auth/login" class="dialog-link unselectable">I don't have an account yet</a>
<input
    type="email"
    class="email-input"
    placeholder="Email"
    bind:value={emailInput}
/>
{#if !StringUtils.isNullOrWhiteSpace(emailEnteringErr)}
    <p class="err-message">{emailEnteringErr}</p>
{/if}
<button class="submit-btn unselectable" on:click={() => confirmEmailInput()}>
    Send Password Update Link
</button>

<style>
    .enter-email-p {
        color: var(--text);
        font-size: 28px;
        margin: 4px 32px;
    }

    .email-input {
        width: 320px;
        box-sizing: border-box;
        margin-top: 48px;
        padding: 4px 8px;
        background-color: var(--back-secondary);
        border: none;
        border-bottom: 2px solid var(--text-faded);
        border-radius: 4px 4px 0 0;
        color: var(--text);
        font-size: 16px;
        outline: none;
        transition: all 0.12s ease;
    }
    .email-input:hover {
        transform: scale(1.04);
    }
    .email-input::placeholder {
        color: var(--text-faded);
    }
    .email-input:focus {
        transform: scale(1.04);
        border-color: var(--primary);
    }
</style>
