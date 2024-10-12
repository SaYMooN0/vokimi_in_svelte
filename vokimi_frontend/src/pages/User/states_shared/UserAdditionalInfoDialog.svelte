<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    export let userId: string;
    let userInfo: UserAdditionalInfo=UserAdditionalInfo.empty();
    let fetchingErr: string = "Unable to fetch data";
    let dialogElement: BaseDialog;
    export async function open() {
        fetchingErr = "";
        const url = "/api/user/getUserAdditionalInfoData" + userId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            userInfo = new UserAdditionalInfo(
                data.realName,
                data.registrationDate,
                data.birthDate,
                data.links,
            );
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "Unknown error";
        }
        dialogElement.open();
    }
</script>

<BaseDialog dialogId="viewUserAdditionalInfoDialog" bind:this={dialogElement}>
    <div class="dialog-body">
        <button class="close-btn" on:click={() => dialogElement.close()}>
            close
        </button>
        <p class="dialog-header">Additional info:</p>
        <div class="prop-val-div">
            <span class="info-prop">Real Name:</span>
            {#if StringUtils.isNullOrWhiteSpace(userInfo.realName)}
                <span class="info-val not-set">(Not set)</span>
            {:else}
                <span class="info-val">{userInfo.realName}</span>
            {/if}
            <span class="info-prop">Birth Date:</span>
            {#if userInfo.birthDate}
                <span class="info-val not-set">(Not set)</span>
            {:else}
                <span class="info-val">{userInfo.birthDate}</span>
            {/if}
            <span class="info-prop">Registration Date:</span>
            {#if userInfo.registrationDate}
                <span class="info-val not-set">(Not set)</span>
            {:else}
                <span class="info-val">{userInfo.registrationDate}</span>
            {/if}
        </div>
        <p class="dialog-header">User links:</p>
        <div class="user-links">
            {#each Object.entries(userInfo.links) as linkKVP}
                {linkKVP}
            {/each}
        </div>
        <slot></slot>
    </div>
</BaseDialog>

<style>
</style>
