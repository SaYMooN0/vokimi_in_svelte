<script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";

    export let userId: string;

    async function followBtnClicked() {
        console.log(notAuthedDialogElement);
        if (notAuthedDialogElement !== undefined) {
            notAuthedDialogElement.open();
            return;
        }
    }
    let notAuthedDialogElement: BaseDialog;
</script>

<button class="follow-btn" on:click={followBtnClicked}>Follow</button>

<AuthorizeView>
    <svelte:fragment slot="unauthenticated">
        <BaseDialog
            dialogId="followUserDialog"
            bind:this={notAuthedDialogElement}
        >
            <div class="dialog-content">
                <CloseButton onClose={() => notAuthedDialogElement.close()} />
                <p>You need to be logged in to follow users</p>
                <a href="/auth/login">Login</a>
            </div>
        </BaseDialog>
    </svelte:fragment>
</AuthorizeView>

<style>
    .follow-btn:hover {
        background-color: var(--primary-hov);
    }
    .dialog-content {
        padding: 20px 48px;
        position: relative;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .dialog-content p {
        margin: 4px;
        font-size: 28px;
        color: var(--text);
    }
    .dialog-content a {
        margin-top: 32px;
        color: var(--back-main);
        background-color: var(--primary);
        padding: 8px 16px;
        border-radius: 4px;
        font-size: 20px;
        cursor: pointer;
    }
    .dialog-content a:hover {
        background-color: var(--primary-hov);
    }
</style>
