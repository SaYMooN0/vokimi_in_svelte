<script lang="ts">
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let receivedResultName: string;
    export let receivedResultImage: string;
    export let receivedResultText: string;
</script>

<div class="received-res-view">
    <p class="your-res-label">Your result:</p>
    <p class="res-name">{receivedResultName}</p>
    <img
        class="received-img"
        src={ImgUtils.imgUrl(receivedResultImage)}
        alt="received-result"
    />
    <p class="res-text">{receivedResultText}</p>
</div>

<style>
    .received-res-view {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .your-res-label {
        font-size: 20px;
        font-weight: 500;
        color: var(--text-faded);
        margin: 0;
    }
    .received-img {
        margin: 4px 0;
        width: 100%;
        max-height: 480px;
        aspect-ratio: 1/1;
        border-radius: 8px;
        object-fit: cover;
    }
    .res-name {
        margin: 4px 0;
        font-size: 28px;
        font-weight: 500;
    }
</style>
