<script lang="ts">
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let conclusionText: string;
    export let conclusionImage: string | null;
    export let anyFeedback: boolean;
</script>

<p class="section-subheader">Conclusion data</p>
<p class="prop-name-val-p">
    <span class="prop-name">Conclusion text:</span>
    <span class="prop-val">{conclusionText}</span>
</p>
<p class="prop-name-val-p">
    <span class="prop-name">Conclusion text:</span>
    {#if StringUtils.isNullOrWhiteSpace(conclusionImage)}
        <span class="prop-val">No image</span>
    {:else}
        <img
            src={ImgUtils.imgUrl(conclusionImage ?? "")}
            alt="Conclusion"
            class="conclusion-image"
        />
    {/if}
</p>
<p class="prop-name-val-p">
    <span class="prop-name">Text feedback:</span>
    <span class="prop-val">
        {anyFeedback ? "Enabled" : "Disabled"}
    </span>
</p>

<style>
    .conclusion-image {
        max-height: 320px;
        max-width: 840px;
        object-fit: contain;
    }
</style>
