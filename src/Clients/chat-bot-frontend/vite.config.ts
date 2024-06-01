import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		host: true,
		port: parseInt(process.env.PORT ?? "5173"),
		headers: {
			'Content-Security-Policy-Report-Only': "default-src 'self' ; report-uri /"
		},
		proxy: {
			'/api': process.env.services__api__https__0 || process.env.services__api__http__0
		}
	}
});
