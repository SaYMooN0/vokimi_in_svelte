<script lang="ts">
    import DraftTestMainInfoView from "./main_info_tab/DraftTestMainInfoView.svelte";
    import DraftTestConclusionView from "./conclusion_tab/DraftTestConclusionView.svelte";
    import DraftTestStylesView from "./styles_tab/DraftTestStylesView.svelte";
    import DraftTestTagsView from "./tags_tab/DraftTestTagsView.svelte";
    import { Route } from "svelte-routing";
    import DraftTestPublishingView from "./publishing_tab/DraftTestPublishingView.svelte";
    import type { TestCreationConclusionTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import type { TestCreationMainInfoTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    import type { TestCreationStylesTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import type { TestCreationTagsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import { TestCreationSettingsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationSettingsTabData";
    import DraftTestSettingsView from "./settings_tab/DraftTestSettingsView.svelte";

    export let testId: string;
    export let updateTestName: (name: string) => void;

    export let mainInfoTabPath: string;
    export let mainInfoTabData: TestCreationMainInfoTabData;

    export let conclusionTabPath: string;
    export let conclusionTabData: TestCreationConclusionTabData;

    export let stylesTabPath: string;
    export let stylesTabData: TestCreationStylesTabData;

    export let settingsTabPath: string;
    export let settingsTabData: TestCreationSettingsTabData;

    export let tagsTabPath: string;
    export let tagsTabData: TestCreationTagsTabData;

    export let publishingTabPath: string;
</script>

<Route path={mainInfoTabPath}>
    <DraftTestMainInfoView
        {updateTestName}
        bind:mainInfoData={mainInfoTabData}
        {testId}
    />
</Route>
<Route path={conclusionTabPath}>
    <DraftTestConclusionView bind:conclusionData={conclusionTabData} {testId} />
</Route>
<Route path={stylesTabPath}>
    <DraftTestStylesView bind:stylesData={stylesTabData} {testId} />
</Route>
<Route path={tagsTabPath}>
    <DraftTestTagsView bind:tagsData={tagsTabData} {testId} />
</Route>
<Route path={settingsTabPath}>
    <DraftTestSettingsView bind:settingsData={settingsTabData} {testId} />
</Route>

<Route path={publishingTabPath}>
    <DraftTestPublishingView {testId} />
</Route>
