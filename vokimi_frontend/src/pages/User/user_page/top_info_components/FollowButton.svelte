<script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let userId: string;
    export let updateParentElement: (
        viewerFollowsUser: boolean,
        userFollowsViewer: boolean,
    ) => void;
    let errMessage: string = "";
    async function followBtnClicked() {
        if (notAuthedDialogElement !== undefined) {
            notAuthedDialogElement.open();
            return;
        }
        const response = await fetch(`/api/userPage/followUser/${userId}`, {
            method: "POST",
        });
        if (response.ok) {
            const data = await response.json();
            updateParentElement(data.viewerFollowsUser, data.userFollowsViewer);
        } else if (response.status === 400) {
            errMessage = await getErrorFromResponse(response);
        } else {
            errMessage = "Unknown error";
        }
    }
    let notAuthedDialogElement: BaseDialog;
</script>

<button class="follow-btn" on:click={followBtnClicked}>
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M6.57757 15.4816C5.1628 16.324 1.45336 18.0441 3.71266 20.1966C4.81631 21.248 6.04549 22 7.59087 22H16.4091C17.9545 22 19.1837 21.248 20.2873 20.1966C22.5466 18.0441 18.8372 16.324 17.4224 15.4816C14.1048 13.5061 9.89519 13.5061 6.57757 15.4816Z"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z"
            stroke="currentColor"
            stroke-width="1.8"
        />
    </svg>
    Follow
</button>
{#if !StringUtils.isNullOrWhiteSpace(errMessage)}
    <span class="err-msg">{errMessage}</span>
{/if}
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
    .err-msg {
        color: var(--red-del);
        font-size: 14px;
    }
</style>
