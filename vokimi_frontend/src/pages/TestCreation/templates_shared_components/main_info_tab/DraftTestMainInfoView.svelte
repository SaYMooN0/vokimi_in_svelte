<script lang="ts">
    import { TestCreationMainInfoTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    import LoadingMessage from "../../../../components/shared/LoadingMessage.svelte";
    export let mainInfoData: TestCreationMainInfoTabData | null = null;
    export let testId: string;
    async function loadTabData() {
        const url = "/api/test-creation/getDraftTestMainInfoData/" + testId;
    }
</script>

<div class="tab-content">
    {#await loadTabData()}
        <LoadingMessage />
    {:then}
        {#if mainInfoData === null}
            <p class="error-message">Please refresh the page</p>
        {:else}
            <div class="left-div">
                <p class="test-template">
                    <span class="property-label"
                        >Test template:
                    </span>{mainInfoData.Template}
                </p>
                <p class="test-name">
                    <span class="property-label">Test name: </span>
                    {mainInfoData.Name}
                </p>
                <p class="test-description">
                    <span class="property-label">Test description: </span>
                    {mainInfoData.Description}
                </p>
                <div class="lang-and-priv-div">
                    <div class="lang-div">
                        <span class="property-label">Language: </span>
                        {mainInfoData.Language}
                    </div>
                    <div class="priv-div">
                        <span class="property-label">Privacy: </span>
                        {mainInfoData.Privacy}
                    </div>
                </div>
            </div>
            <div class="right-div">
                <div class="img-container">
                    <img src={mainInfoData.ImgPath} alt="test cover" />
                    <div class="img-operations-btn change-img-btn">
                        Change Test Cover
                    </div>
                    <div class="img-operations-btn remove-img-btn">
                        Set Back To Default
                    </div>
                </div>
            </div>
        {/if}
    {/await}
</div>
