import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		host: true,
		port: parseInt(process.env.PORT ?? "5173"),
		headers: {
			'Content-Security-Policy-Report-Only': "default-src 'self' ; report-uri /"
		}
	}
});
