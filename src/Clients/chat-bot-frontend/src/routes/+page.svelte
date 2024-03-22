<script lang="ts">
import QuestionList from "../components/QuestionList/QuestionList.svelte";
import Banner from "../components/Banner/Banner.svelte";
import FaqSection from "../components/Section/FaqSection.svelte";
import {search} from "../lib/api.js";
import {onMount} from "svelte";
import Loader from "../components/Loader/Loader.svelte";

let questions: QuestionListElement[] = undefined;

const fetchQuestions = async(): Promise<void> => {
	const data = await search(`orderBy=CreatedAt&page=0&take=10`);

	questions = data.questions.map((q) => ({
		id: q.id,
		summary: q.summary,
		title: q.title,
		createdAt: new Date(q.createdAt)
	}));
}

onMount(async () => {
	await fetchQuestions();
});


const title = "Recent questions";

</script>

<svelte:head>
	<title>AvatarUI</title>
	<meta name="description" content="AvatarUI" />
</svelte:head>

<section class="dark:bg-[#121C24] dark:border-white ">
	<Banner/>
	<FaqSection/>
	{#if questions === undefined}
	<Loader/>
	{:else}
	<QuestionList {title} {questions}/>
	{/if}
</section>

