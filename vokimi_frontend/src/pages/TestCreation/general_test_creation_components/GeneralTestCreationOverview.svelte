<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    import GeneralTestQuestionsView from "./general_test_overview_tabs/questions_tab/GeneralTestQuestionsView.svelte";
    import GeneralTestResultsView from "./general_test_overview_tabs/results_tab/GeneralTestResultsView.svelte";

    export let basepath: string;
    export const GeneralTestCreationOverviewTabs = {
        "Main Info": "main-info",
        Questions: "view-questions",
        Results: "view-results",
        Styles: "view-styles",
        Conclusion: "view-conclusion",
    };
    console.log(GeneralTestCreationOverviewTabs, basepath);
</script>

<div class="overview-tabs-container">
    <Router {basepath}>
        <SharedTestCreationRoutes
            viewStylesPath={GeneralTestCreationOverviewTabs["Styles"]}
            viewConclusionPath={GeneralTestCreationOverviewTabs["Conclusion"]}
        />
        <Route
            path={GeneralTestCreationOverviewTabs["Questions"]}
            component={GeneralTestQuestionsView}
        />
        <Route
            path={GeneralTestCreationOverviewTabs["Results"]}
            component={GeneralTestResultsView}
        />
        <Route>An error has occured. Please go to a previous page</Route>
    </Router>
</div>
