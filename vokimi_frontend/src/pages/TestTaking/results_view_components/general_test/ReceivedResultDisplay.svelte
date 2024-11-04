<script lang="ts">
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";


    export let receivedResultName: string;
    export let receivedResultImage: string | null;
    export let receivedResultText: string;
</script>

<div class="received-res-view">
    <h2 class="your-res-label">Your result:</h2>
    {#if !StringUtils.isNullOrWhiteSpace(receivedResultImage)}
        <img
            class="received-img"
            src={ImgUtils.imgUrl(receivedResultImage ?? "")}
            alt="received-result"
        />
    {/if}
    <p class="res-name">{receivedResultName}</p>
    <p class="res-text">{receivedResultText}</p>
</div>

<style>
    .received-res-view {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
