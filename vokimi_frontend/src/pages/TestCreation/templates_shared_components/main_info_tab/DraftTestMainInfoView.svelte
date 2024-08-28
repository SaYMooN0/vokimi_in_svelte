<script lang="ts">
    import { TestCreationMainInfoTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    import LoadingMessage from "../../../../components/shared/LoadingMessage.svelte";
    export let mainInfoData: TestCreationMainInfoTabData;
    export let testId: string;
    async function loadTabData() {
        if (!mainInfoData.isEmpty()) {
            return;
        }
        const url = "/api/test-creation/getDraftTestMainInfoData/" + testId;

        const response = await fetch(url);

        if (response.ok) {
            const data = await response.json();
            console.log(data);
            mainInfoData.update(
                data.template,
                data.name,
                data.description,
                data.language,
                data.privacy,
                data.imgPath,
            );
        } else {
            mainInfoData = TestCreationMainInfoTabData.empty();
        }
    }
</script>

<div class="tab-content">
    {#await loadTabData()}
        <LoadingMessage />
    {:then}
        {#if mainInfoData.isEmpty()}
            <p class="error-message">Please refresh the page</p>
        {:else}
            <div class="left-div">
                <p class="test-template">
                    <span class="property-label"
                        >Test template:
                    </span>{mainInfoData.template}
                </p>
                <p class="test-name">
                    <span class="property-label">Test name: </span>
                    {mainInfoData.name}
                </p>
                <p class="test-description">
                    <span class="property-label">Test description: </span>
                    {#if mainInfoData.description === ""}
                        (No description)
                    {:else}
                        {mainInfoData.description}
                    {/if}
                </p>
                <div class="lang-and-priv-div">
                    <div class="lang-div">
                        <span class="property-label">Language: </span>
                        {mainInfoData.language}
                    </div>
                    <div class="priv-div">
                        <span class="property-label">Privacy: </span>
                        {mainInfoData.privacy}
                    </div>
                </div>
            </div>
            <div class="right-div">
                <div class="img-container">
                    <img src={mainInfoData.imgPath} alt="test cover" />
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
