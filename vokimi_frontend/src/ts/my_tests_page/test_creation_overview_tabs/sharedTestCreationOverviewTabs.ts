import MainInfoIconSvg from "../../../pages/TestCreation/tab_links_icons/MainInfoIconSvg.svelte";
import ConclusionIconSvg from "../../../pages/TestCreation/tab_links_icons/ConclusionIconSvg.svelte";
import StylesIconSvg from "../../../pages/TestCreation/tab_links_icons/StylesIconSvg.svelte";
import TagsIconSvg from "../../../pages/TestCreation/tab_links_icons/TagsIconSvg.svelte";
import PublishSvgIcon from "../../../pages/TestCreation/tab_links_icons/PublishSvgIcon.svelte";

export const SharedTestCreationOverviewTabs = {
    "Main Info": {
        id: "main-info-view",
        icon: MainInfoIconSvg,
    },
    Styles: {
        id: "view-styles",
        icon: StylesIconSvg,
    },
    Conclusion: {
        id: "view-conclusion",
        icon: ConclusionIconSvg,
    },
    Tags: {
        id: "view-tags",
        icon: TagsIconSvg,
    },
    Publish: {
        id: "view-publish",
        icon: PublishSvgIcon
    }
};
