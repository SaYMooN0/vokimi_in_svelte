<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import SignupForm from "./SignupForm.svelte";
    import AlreadyLoggedIn from "../../components/AlreadyLoggedIn.svelte";
    import LinkHasBeenSentMessage from "./LinkHasBeenSentMessage.svelte";

    let currentStateForm: boolean = true;
    let linkHasBeenSentMessageComponent: LinkHasBeenSentMessage;

    function ChangeStateToLinkHasBeenSentMessage(
        usernameVal: string,
        emailVal: string,
    ) {
        currentStateForm = false;
        linkHasBeenSentMessageComponent.SetUsernameAndEmail(
            usernameVal,
            emailVal,
        );
    }
</script>

<AuthorizeView>
    <div slot="loading">
        <span>Checking Authentication</span>
    </div>
    <div slot="authenticated">
        <AlreadyLoggedIn />
    </div>
    <div slot="unauthenticated">
        {#if currentStateForm}
            <SignupForm {ChangeStateToLinkHasBeenSentMessage} />
        {:else}
            <LinkHasBeenSentMessage
                bind:this={linkHasBeenSentMessageComponent}
            />
        {/if}
    </div>
</AuthorizeView>
