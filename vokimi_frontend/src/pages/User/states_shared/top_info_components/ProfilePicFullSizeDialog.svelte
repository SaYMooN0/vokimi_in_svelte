<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";

    export let profilePicture: string;
    export function open() {
        dialogElement.open();
    }
    let dialogElement: BaseDialog;
</script>

<BaseDialog dialogId="profilePicFullSizeDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <img src={ImgUtils.imgUrl(profilePicture)} alt="Profile Pic" />
        <button on:click={() => dialogElement.close()}>close</button>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        padding: 8px;
        position: relative;
    }
    .dialog-content img {
        min-height: 240px;
        min-width: 320px;
        max-width: 86vw;
        max-height: 86vh;
        object-fit: contain;
        border-radius: 8px;
    }
    .dialog-content button {
        position: absolute;
        top: 12px;
        right: 12px;
    }
</style>
