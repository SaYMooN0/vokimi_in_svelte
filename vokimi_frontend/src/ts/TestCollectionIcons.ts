import Bookmark from "../components/icons/collection_icons/ci_Bookmark.svelte";
import Clock from "../components/icons/collection_icons/ci_Clock.svelte";
import Folder from "../components/icons/collection_icons/ci_Folder.svelte";
import Heart from "../components/icons/collection_icons/ci_Heart.svelte";
import Star from "../components/icons/collection_icons/ci_Star.svelte";
import Study from "../components/icons/collection_icons/ci_Study.svelte";
import Brain from "../components/icons/collection_icons/ci_Brain.svelte";
import type { SvelteComponent } from "svelte";

export let CollectionIconsIdComponentObj: Record<string, any> = {
    default: Folder,
    bookmark: Bookmark,
    heart: Heart,
    clock: Clock,
    star: Star,
    brain: Brain,
    study: Study,
};
