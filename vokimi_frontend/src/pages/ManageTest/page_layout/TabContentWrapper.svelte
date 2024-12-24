<script lang="ts">
    import { Err } from "../../../ts/Err";
    import TabDataFetchingErrDiv from "../tabs_shared/TabDataFetchingErrDiv.svelte";

    export let fetchTabData: () => Promise<Err>;
    export let isActive: boolean;
    let dataFetchingErr: Err = new Err("Data not fetched.");

    async function forcedSetTabData(): Promise<Err> {
        dataFetchingErr = await fetchTabData();
        return dataFetchingErr;
    }
    async function activeTabOnSwitchSetTabData() {
        if (dataFetchingErr.notNone()) {
            dataFetchingErr = await forcedSetTabData();
        }
    }
    $: if (isActive) {
        activeTabOnSwitchSetTabData();
    }
</script>

<div class="tab-content" class:activeTabContent={isActive}>
    {#if isActive}
        {#if dataFetchingErr.notNone()}
            <TabDataFetchingErrDiv
                err={dataFetchingErr.toString()}
                tryAgainAction={() => forcedSetTabData()}
            />
        {:else}
            <slot updateTabData={() => forcedSetTabData()} />
        {/if}
    {/if}
</div>

<style>
    .tab-content {
        display: none;
    }
    .tab-content.activeTabContent {
        display: block;
        box-sizing: border-box;
        padding: 4px 12px;
    }
</style>
