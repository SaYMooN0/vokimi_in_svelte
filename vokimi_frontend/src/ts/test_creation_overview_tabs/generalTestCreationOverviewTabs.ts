import QuestionIconSvg from '../../pages/TestCreation/tab_links_icons/QuestionIconSvg.svelte';
import ResultIconSvg from '../../pages/TestCreation/tab_links_icons/ResultIconSvg.svelte';
import { SharedTestCreationOverviewTabs } from './sharedTestCreationOverviewTabs';

export const GeneralTestCreationOverviewTabs = {
    "Main Info": SharedTestCreationOverviewTabs["Main Info"],
    Questions: {
        id: "view-questions",
        icon: QuestionIconSvg,
    },
    Results: {
        id: "view-results",
        icon: ResultIconSvg,
    },
    Styles: SharedTestCreationOverviewTabs.Styles,
    Conclusion: SharedTestCreationOverviewTabs.Conclusion,
    Tags: SharedTestCreationOverviewTabs.Tags,
    Publish: SharedTestCreationOverviewTabs.Publish
};
