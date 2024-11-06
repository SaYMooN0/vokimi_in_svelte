<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    import GeneralTestQuestionsView from "./general_test_overview_tabs/questions_tab/GeneralTestQuestionsView.svelte";
    import GeneralTestResultsView from "./general_test_overview_tabs/results_tab/GeneralTestResultsView.svelte";
    import { TestCreationMainInfoTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    import { GeneralTestCreationQuestionsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/questions/GeneralTestCreationQuestionsTabData";
    import { GeneralTestCreationResultsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/results/GeneralTestCreationResultsTabData";
    import { TestCreationConclusionTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import { TestCreationStylesTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import { TestCreationTagsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import { TestCreationSettingsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationSettingsTabData";
    export let basepath: string;
    export let testId: string;
    export let updateTestName: (name: string) => void;

    let mainInfoTabData: TestCreationMainInfoTabData =
        TestCreationMainInfoTabData.empty();
    let questionsTabData: GeneralTestCreationQuestionsTabData =
        GeneralTestCreationQuestionsTabData.empty();
    let resultsTabData: GeneralTestCreationResultsTabData =
        GeneralTestCreationResultsTabData.empty();
    let conclusionTabData: TestCreationConclusionTabData =
        TestCreationConclusionTabData.empty();
    let stylesTabData: TestCreationStylesTabData =
        TestCreationStylesTabData.empty();
    let tagsTabData: TestCreationTagsTabData = TestCreationTagsTabData.empty();

    let settingsTabData: TestCreationSettingsTabData =
        TestCreationSettingsTabData.empty();
</script>

<Router {basepath}>
    <SharedTestCreationRoutes
        {testId}
        {updateTestName}
        mainInfoTabPath="main-info-view"
        bind:mainInfoTabData
        conclusionTabPath="view-conclusion"
        bind:conclusionTabData
        stylesTabPath="view-styles"
        bind:stylesTabData
        settingsTabPath="view-settings"
        bind:settingsTabData
        tagsTabPath="view-tags"
        bind:tagsTabData
        publishingTabPath="view-publish"
    />
    <Route path="view-questions">
        <GeneralTestQuestionsView
            {testId}
            bind:questionsData={questionsTabData}
        />
    </Route>
    <Route path="view-results">
        <GeneralTestResultsView {testId} bind:resultsData={resultsTabData} />
    </Route>
    <Route>
        <p class="go-back-error-message">
            An error has occurred. Please go to a previous page
        </p>
    </Route>
</Router>
